module PersistenceInterface
open Microsoft.Data.Sqlite

let connection_string = "Data Source=database/tatalu.db"

type Sqlite private() = 
    static let connection = new SqliteConnection(connection_string)
    
    static let instance = Sqlite()

    static member Instance = instance

    member self.Connection = connection


let excute_with_table (sql: string) (inserter: (SqliteCommand -> Unit) option) (parser: SqliteDataReader -> 'a) = 
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

let excute_without_table (sql: string) (inserter: (SqliteCommand -> Unit) option) = 
    let sqlite = Sqlite.Instance

    use connection = sqlite.Connection

    connection.Open()

    use command  = connection.CreateCommand()

    command.CommandText <- sql

    match inserter with
    | Some fn -> fn command
    | None -> ()
    
    try 
        Ok  (command.ExecuteNonQuery())
    with
    | :? Microsoft.Data.Sqlite.SqliteException as ex -> Error (ex.ToString())

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

    excute_with_table sql (Some inserter) parser
    


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

    excute_with_table sql inserter parser


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
        
    excute_with_table sql inserter parser
   
let insert_card title list_id = 
    let sql = 
        """
            insert into
                card(title, list_id)
            values
                ($title, $list_id)
        """

    let inserter = 
        (fun (command: SqliteCommand) ->
            command.Parameters.AddWithValue("$title", title) |> ignore
            command.Parameters.AddWithValue("$list_id", list_id) |> ignore
        )
        |> Some

    match excute_without_table sql inserter with
    | Ok t -> Ok "完成。"
    | Error e -> "错误：数据库发生未知异常。" |> Error

let insert_list title kanban_id = 
    let sql = 
        """
            insert into 
                list(title, kanban_id)
            values
                ($title, $kanban_id)

        """

    let inserter = 
        (
            fun (command: SqliteCommand) ->
                command.Parameters.AddWithValue("$title", title) |> ignore
                command.Parameters.AddWithValue("$kanban_id", kanban_id) |> ignore

        )
        |> Some

    match excute_without_table sql inserter with
    | Ok t -> Ok "完成。"
    | Error e -> "错误：数据库发生未知异常。" |> Error

let insert_kanban title = 
    let sql = 
        """
            insert into
                kanban(title)
            values
                ($title)
        """

    let inserter = Some (fun (command: SqliteCommand) -> command.Parameters.AddWithValue("$title", title) |> ignore)

    match excute_without_table sql inserter with
    | Ok t -> Ok "完成。"
    | Error e -> "错误：数据库发生未知异常。" |> Error

let delete_card id = 
    let sql = 
        """
            delete from
                card
            where
                id = $id
        """

    let inserter = 
        (fun (command: SqliteCommand) -> 
            command.Parameters.AddWithValue("$id", id) |> ignore
        )
        |> Some

    match excute_without_table sql inserter with
    | Ok t -> Ok "完成。"
    | Error e -> "错误：数据库发生未知异常。" |> Error 

let delete_list id = 
    let sql = 
        """
            delete from
                list
            where
                id = $id
        """

    let inserter = 
        (fun (command: SqliteCommand) -> 
            command.Parameters.AddWithValue("$id", id) |> ignore
        )
        |> Some

    match excute_without_table sql inserter with
    | Ok t -> Ok "完成"
    | Error e -> $"错误：编号为%i{id}的列表下存在未删除的卡片" |> Error 


let delete_kanban id = 
    let sql = 
        """
            delete from
                kanban
            where
                id = $id
        """

    let inserter = 
        (fun (command: SqliteCommand) -> 
            command.Parameters.AddWithValue("$id", id) |> ignore
        )
        |> Some

    match excute_without_table sql inserter with
    | Ok t -> Ok "完成"
    | Error e -> $"错误：编号为%i{id}的看板下存在未删除的列表" |> Error

let update_card_list_id_by_id id list_id = 
    let sql = 
        """
            update 
                card
            set
                list_id = $list_id
            where
                id = $id
        """

    let inserter = 
        (fun (command: SqliteCommand) ->
            command.Parameters.AddWithValue("$list_id", list_id) |> ignore
            command.Parameters.AddWithValue("$id", id) |> ignore
        )
        |> Some

    match excute_without_table sql inserter with
    | Ok t -> Ok "完成"
    | Error e -> $"错误：不存在编号为%i{list_id}的列表" |> Error

let update_card_title_by_id id title = 
    let sql = 
        """
            update 
                card
            set 
                title = $title
            where
                id = $id
        """

    let inserter = 
        (fun (command: SqliteCommand) ->
            command.Parameters.AddWithValue("$title", title) |> ignore
            command.Parameters.AddWithValue("$id", id) |> ignore
        )
        |> Some

    match excute_without_table sql inserter with
    | Ok t -> Ok "完成"
    | Error e -> "错误：数据库发生未知异常" |> Error

let update_list_title_by_id id title = 
    let sql = 
        """
            update 
                list
            set 
                title = $title
            where
                id = $id
        """

    let inserter = 
        (fun (command: SqliteCommand) ->
            command.Parameters.AddWithValue("$title", title) |> ignore
            command.Parameters.AddWithValue("$id", id) |> ignore
        )
        |> Some

    match excute_without_table sql inserter with
    | Ok t -> Ok "完成"
    | Error e -> "错误：数据库发生未知异常" |> Error


let update_kanban_title_by_id id title = 
    let sql = 
        """
            update 
                kanban
            set 
                title = $title
            where
                id = $id
        """

    let inserter = 
        (fun (command: SqliteCommand) ->
            command.Parameters.AddWithValue("$title", title) |> ignore
            command.Parameters.AddWithValue("$id", id) |> ignore
        )
        |> Some

    match excute_without_table sql inserter with
    | Ok t -> Ok "完成"
    | Error e -> "错误：数据库发生未知异常" |> Error
