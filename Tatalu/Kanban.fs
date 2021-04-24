module Kanban

let create id title seq = 
    {
        DomainTypes.Kanban.id = id;
        DomainTypes.Kanban.title = title;
        DomainTypes.Kanban.lists = seq;
    }

let debug (kanban: DomainTypes.Kanban) = 
    printfn "[ %i ][ %s ]" kanban.id kanban.title

    match kanban.lists with
    | None -> printfn ""
    | Some kb -> 
        kb
        |> Map.iter (fun k v -> List.debug v)
