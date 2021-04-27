module PersistenceInterface
open Microsoft.Data.Sqlite

let connection_string = "Data Source=database/tatalu.db"

type Sqlite private() = 
    static let connection = new SqliteConnection(connection_string)
    
    static let instance = Sqlite()

    static member Instance = instance

    member self.Connection = connection


let select (sql: string) (inserter: (SqliteCommand -> Unit) option) (parser: SqliteDataReader -> 'a) = 
    let sqlite = Sqlite.Instance
    
    use connection = sqlite.Connection
        
    connection.Open()
    
    use command = connection.CreateCommand()
    
    command.CommandText <- sql
    
    match inserter with
    | Some fn -> fn command
    | None -> ()

    use reader = command.ExecuteReader()
    
    let list = List.empty
    
    let rec read (reader: SqliteDataReader) list = 
        match reader.Read() with
        | true -> read reader ((parser reader)::list)
        | false -> list
    
    read reader list

let select_all_cards_from_card_by_list_id id = 
    let sql = 
        """
        select
            card.id,
            card.title,
            card.list_id
        from
            card
        where
            list_id = $id
        """

    let inserter (command: SqliteCommand) = 
        command.Parameters.AddWithValue("$id", id) |> ignore

    let parser (reader: SqliteDataReader) = 
        (reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2))

    select sql (Some inserter) parser
    


let select_all_kanbans_from_Kanban _ = 

    let sql = 
        """
            select
                *
            from
                Kanban
        """

    let inserter = None

    let parser (reader: SqliteDataReader) = 
        (reader.GetInt32(0), reader.GetString(1))

    select sql inserter parser


let select_all_lists_from_list_by_kanban_id id = 
    
    let sql =
        """
            select
                list.id,
                list.title,
                list.kanban_id
            from
                list
            where
                kanban_id = $id
        """


    let inserter = Some (fun (command: SqliteCommand) -> command.Parameters.AddWithValue("$id", id) |> ignore)

    let parser (reader: SqliteDataReader) = 
        (reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2))
        
    select sql inserter parser
