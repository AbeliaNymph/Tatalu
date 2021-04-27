module PresentationTypes

let drawing_down_and_right = "┌"
let drawing_horizontal = "─"
let drawing_vertical = "|"
let drawing_down_and_left = "┐"

type KanbanDashboard = 
    {
        title: string;
        row_width: int;
        row_id_width: int;
        row_title_width: int;
        list: DomainTypes.Kanban list
    }