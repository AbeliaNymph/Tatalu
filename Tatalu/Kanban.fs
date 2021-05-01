module Kanban

let create id title =
    {
        DomainTypes.Kanban.id = id;
        DomainTypes.Kanban.title = title;
    }

let load_from_database _ =
    PersistenceInterface.select_all_kanbans_from_Kanban()
    |> List.map (fun (id, title) -> create id title)

let insert_into_database title = 
    match PersistenceInterface.insert_kanban title with
    | Ok t -> printfn "%s" t
    | Error e -> eprintfn "%s" e


let secondary_verification _ = 
    printfn "删除看板将删除列表下的所有列表，此操作不可恢复。确认（y/n）?"
    
    match System.Console.ReadLine().ToLower() with
    | "y" -> true
    | _ -> false

let delete_from_database id = 
    List.load_lists_from_database id
    |> List.iter (fun list -> 
        List.delete_from_database list.id
    )

    PersistenceInterface.delete_kanban id

let modify_title id title = 
    PersistenceInterface.update_kanban_title_by_id id title
    
