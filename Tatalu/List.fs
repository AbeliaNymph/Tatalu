module List

let create id title kanban_id = 
    {
        DomainTypes.List.id = id;
        DomainTypes.List.title = title;
        DomainTypes.List.kanban_id = kanban_id;
    }


let load_lists_from_database kanban_id = 
    PersistenceInterface.select_all_lists_from_list_by_kanban_id kanban_id
    |> List.map (fun (id, title, kanban_id) -> create id title kanban_id)

let insert_into_database title kanban_id = 
    match PersistenceInterface.insert_list title kanban_id with
    | Ok t -> printfn "%s" t
    | Error e -> eprintfn "%s" e


let secondary_verification _ = 
    printfn "删除列表将删除列表下的所有卡片，此操作不可恢复。确认（y/n）?"
    
    match System.Console.ReadLine().ToLower() with
    | "y" -> true
    | _ -> false


let delete_from_database id = 
    
    Card.load_cards_from_database id
    |> List.iter (fun card -> Card.delete_from_database card.id)

    match PersistenceInterface.delete_list id with
    | Ok t -> printfn "%s" t
    | Error e -> eprintfn "%s" e

let modify_title id title = 
    PersistenceInterface.update_list_title_by_id id title
    

    
