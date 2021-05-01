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
                Kanban.load_from_database()
                |> KanbanDashboard.ofKanbanList
                |> KanbanDashboard.display
                
            
                ()

            | _ -> Help.kanban()

        | _ -> Help.tatalu()

    | 3 ->
        match argv.[0] with
        | "kanban" ->
            match argv.[1] with
            | "create" ->
                Kanban.insert_into_database argv.[2]
            | "delete" ->
                match KanbanDashboard.secondary_verification() with
                | true -> 
                    Kanban.delete_from_database (int argv.[2])
                    |> KanbanDashboard.display_result
                | false -> printfn "取消"
            | _ -> Help.kanban()
        
        
        | "list" ->
            match argv.[1] with           
            | "list" ->
                List.load_lists_from_database (int argv.[2])
                |> ListDashboard.ofListsList
                |> ListDashboard.display

            | "delete" ->
                match List.secondary_verification() with
                | true -> List.delete_from_database (int argv.[2])
                | false -> printfn "取消"   

            | _ -> Help.list()

        | "card" ->
            match argv.[1] with
            | "list" ->
                Card.load_cards_from_database (int argv.[2])
                |> CardDashboard.ofCardList
                |> CardDashboard.display

            | "delete" ->
                Card.delete_from_database (int argv.[2])

            | _ -> Help.card()
        | _ -> Help.tatalu()

    | 4 ->
        match argv.[0] with
        | "list" ->
            match argv.[1] with 
            | "create" -> 
                List.insert_into_database argv.[2] (int argv.[3])
            
            | "modify" ->
                List.modify_title (int argv.[2]) argv.[3]
                |> ListDashboard.display_result
            
            | _ -> Help.list()

        | "card" ->
            match argv.[1] with
            | "create" ->
                Card.insert_into_database argv.[2] (int argv.[3])

            | "modify" ->
                Card.modify_title (int argv.[2]) argv.[3]
                |> CardDashboard.display_result
            
            | "move" ->
                Card.move_to_another_list (int argv.[2]) (int argv.[3])
                |> CardDashboard.display_result
            
            | _ -> Help.card()


        | "kanban" ->
            match argv.[1] with
            | "modify" ->
                Kanban.modify_title (int argv.[2]) argv.[3]
                |> KanbanDashboard.display_result

            | _ -> Help.kanban()
        
        | _ -> Help.tatalu()
    
    | _ -> Help.tatalu()




    0
