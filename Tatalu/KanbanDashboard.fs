module KanbanDashboard

let ofKanbanList kanban_list = 
    {
        PresentationTypes.KanbanDashboard.title = "看板仪表板";
        PresentationTypes.KanbanDashboard.list = kanban_list;
    }

let secondary_verification _ = 
    printfn "删除列表将删除列表下的所有卡片，此操作不可恢复。确认（y/n）?"
    
    match System.Console.ReadLine().ToLower() with
    | "y" -> true
    | _ -> false

let display (kanban_dashboard: PresentationTypes.KanbanDashboard) = 
    printfn "# %s" kanban_dashboard.title
    
    kanban_dashboard.list
    |> List.rev
    |> List.iter (fun kanban -> printfn "\t-[编号 = %i][标题 = %s]" kanban.id kanban.title
    )

let display_result result = 
    match result with
    | Ok t -> printfn "%s" t
    | Error e -> eprintfn "%s" e