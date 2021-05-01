module PresentationTypes

let drawing_down_and_right = "┌"
let drawing_horizontal = "─"
let drawing_vertical = "|"
let drawing_down_and_left = "┐"

type KanbanDashboard = 
    {
        title: string;
        list: DomainTypes.Kanban list
    }

type ListDashboard = 
    {
        title: string;
        list: DomainTypes.List list
    }

type CardDashboard = 
    {
        title: string;
        list: DomainTypes.Card list
    }