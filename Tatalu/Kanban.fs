module Kanban

let create id title =
    {
        DomainTypes.Kanban.id = id;
        DomainTypes.Kanban.title = title;
    }

let load_from_database _ =
    PersistenceInterface.select_all_kanbans_from_Kanban()
    |> List.map (fun (id, title) -> create id title)
    
