module KanbanDashboard

let ofKanbanList kanban_list = 
    let row_id_width = 
        kanban_list
        |> List.fold (fun max_length (kanban: DomainTypes.Kanban) -> 
            if kanban.id.ToString().Length > max_length then kanban.id.ToString().Length else max_length
        ) 0

    let row_title_width = 
        kanban_list
        |> List.fold (fun max_length (kanban: DomainTypes.Kanban) -> 
            if kanban.title.Length > max_length then kanban.title.Length else max_length
        ) 0
    
    {
        PresentationTypes.KanbanDashboard.title = "Kanban Dashboard";
        PresentationTypes.KanbanDashboard.row_width = row_id_width + row_title_width + 3;
        PresentationTypes.KanbanDashboard.row_id_width = row_id_width;
        PresentationTypes.KanbanDashboard.row_title_width = row_title_width;
        PresentationTypes.KanbanDashboard.list = kanban_list;
    }

let display (kanban_dashboard: PresentationTypes.KanbanDashboard) = 
    printf "%s" PresentationTypes.drawing_down_and_right
    
    

    for _ in 1..kanban_dashboard.row_width do
        printf "%s" PresentationTypes.drawing_horizontal

    printfn "%s" PresentationTypes.drawing_down_and_left

    printf "%s" PresentationTypes.drawing_vertical

    printf "%s" "id"
