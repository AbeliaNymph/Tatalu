open System
open Microsoft.Data.Sqlite




[<EntryPoint>]
let main argv =

    match argv.Length with
    | 1 -> 
        match argv.[0] with
        | "-h" | "--help" -> Help.tatalu()
        | _ -> Help.tatalu()
    
    | 2 ->
        match argv.[0] with
        | "kanban" ->
            match argv.[1] with
            | "list" ->
                //Command.kanban_list()
                //Database.select_all_kanbans_from_Kanban()
                //|> List.map (fun (id, title) -> Model.Kanban.create id title None)
                //|> List.map (fun kanban -> 
                //    {
                //        kanban with lists = 
                //            Database.select_all_lists_from_list_by_kanban_id kanban.id
                //            |> List.map (fun (list_id, list_title) -> Model.List.create list_id list_title None)
                //            |> List.map (fun list -> 
                //                {
                //                    list with cards = 
                //                        Database.select_all_cards_from_card_by_list_id list.id
                //                        |> List.map (fun (card_id, card_title) -> Model.Card.create card_id card_title)
                //                        |> List.map (fun card -> card.title,card)
                //                        |> Seq.ofList
                //                        |> Map
                //                        |> Some
                //                }
                //            )
                //            |> List.map (fun list -> list.title, list)
                //            |> Seq.ofList
                //            |> Map
                //            |> Some
                //    }
                //)
                //|> List.iter (fun kanban -> Model.Kanban.display kanban)
            
                ()

            | _ -> eprintfn "command not found."

        | _ -> eprintfn "command not found."
    | _ -> Help.tatalu()




    0
