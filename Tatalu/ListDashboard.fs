module ListDashboard

let ofListsList lists_list = 
    {
        PresentationTypes.ListDashboard.title = "列表仪表板";
        PresentationTypes.ListDashboard.list = lists_list;
    }

let display (list_dashboard: PresentationTypes.ListDashboard) =
    printfn "## %s" list_dashboard.title
    
    list_dashboard.list
    |> List.rev
    |> List.iter (fun list -> printfn "\t-[编号 = %i][标题 = %s]" list.id list.title
    )

let display_result result = 
    match result with
    | Ok t -> printfn "%s" t
    | Error e -> eprintfn "%s" e