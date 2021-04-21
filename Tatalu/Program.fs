open System
open Microsoft.Data.Sqlite

let help _ =
    """Usage: Tatalu <command> [<args>]
        
    """
    |> printfn "%s"


[<EntryPoint>]
let main argv =

    match argv.Length with
    | 2 ->
        match argv.[0] with
        | "kanban" ->
            match argv.[1] with
            | "list" ->
                Database.select_all_kanbans_from_Kanban()
                |> List.map (fun (id, title) -> Model.Kanban.create id title None)
                |> List.map (fun kanban -> 
                    {
                        kanban with lists = 
                            Database.select_all_lists_from_list_by_kanban_id kanban.id
                            |> List.map (fun (list_id, list_title) -> Model.List.create list_id list_title None)
                            |> List.map (fun list -> 
                                {
                                    list with cards = 
                                        Database.select_all_cards_from_card_by_list_id list.id
                                        |> List.map (fun (card_id, card_title) -> Model.Card.create card_id card_title)
                                        |> List.map (fun card -> card.title,card)
                                        |> Seq.ofList
                                        |> Map
                                        |> Some
                                }
                            )
                            |> List.map (fun list -> list.title, list)
                            |> Seq.ofList
                            |> Map
                            |> Some
                    }
                )
                |> printfn "%A"
                
            | _ -> eprintfn "command not found."

        | _ -> eprintfn "command not found."
    | _ -> help()


    //use connection = new SqliteConnection("Data Source=database/tatalu.db")
    
    //connection.Open()

    //use command = connection.CreateCommand()

    //command.CommandText <- 
    //    """
    //        select 
    //            *
    //        from
    //            Kanban
    //        where
    //            id = $id
    //    """
    //command.Parameters.AddWithValue("$id", 1) |> ignore

    //use reader = command.ExecuteReader()

    //while reader.Read() do
    //    let id = reader.GetInt32(0)
    //    let title = reader.GetString(1)

    //    printfn "%A: %A" id title

    
    
    //let manager = 
    //    kanbans
    //    |> List.map (

    //        fun (kanban_id, kanban_title) ->

    //            let list_seq = 
    //                Database.select_all_lists_from_list_by_kanban_id kanban_id
    //                |> List.map (

    //                    fun (list_id, list_title) ->

    //                        let card_seq = 
    //                            Database.select_all_cards_from_card_by_list_id list_id
    //                            |> List.map (

    //                                fun (card_id, card_title) ->
    //                                    (card_title, Model.Card.create card_id card_title)
    //                            )
    //                            |> List.toSeq

    //                        (list_title, Model.List.create list_id list_title card_seq)
                    
    //                )
    //                |> List.toSeq

    //            (kanban_title, Model.Kanban.create kanban_id kanban_title list_seq)
            
    //    )
    //    |> Model.Manager.create


    //let mutable current_kanban = Model.Kanban.empty ()
    //match manager.kanbans.TryFind "test kanban" with
    //| Some k -> current_kanban <- k
    //| None -> eprintfn "Kanban not found"
    //printfn "%A" manager



    0
