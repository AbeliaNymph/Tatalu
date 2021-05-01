module CardDashboard

let ofCardList card_list = 
    {
        PresentationTypes.CardDashboard.title = "卡片仪表板";
        PresentationTypes.CardDashboard.list = card_list;
    }

let display (card_dashboard: PresentationTypes.CardDashboard) =
    printfn "## %s" card_dashboard.title
    
    card_dashboard.list
    |> List.rev
    |> List.iter (fun card -> printfn "\t-[编号 = %i][标题 = %s]" card.id card.title
    )

let display_result result = 
    match result with
    | Ok t -> printfn "%s" t
    | Error e -> eprintfn "%s" e