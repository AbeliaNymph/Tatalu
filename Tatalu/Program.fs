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

            | _ -> eprintfn "command not found."

        | _ -> eprintfn "command not found."
    | _ -> Help.tatalu()




    0
