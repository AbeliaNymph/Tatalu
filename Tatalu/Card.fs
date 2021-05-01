module Card

let create id title list_id = 
    {
        DomainTypes.Card.id = id;
        DomainTypes.Card.title = title;
        DomainTypes.Card.list_id = list_id;
    }

let load_cards_from_database list_id = 
    PersistenceInterface.select_all_cards_from_card_by_list_id list_id
    |> List.map (fun (id, title, list_id) -> create id title list_id)

let insert_into_database title list_id = 
    match PersistenceInterface.insert_card title list_id with
    | Ok t -> printfn "%s" t
    | Error e -> eprintfn "%s" e

let delete_from_database id = 
    match PersistenceInterface.delete_card id with
    | Ok t -> printfn "%s" t
    | Error e -> eprintfn "%s" e

let move_to_another_list id list_id = 
    PersistenceInterface.update_card_list_id_by_id id list_id

let modify_title id title = 
    PersistenceInterface.update_card_title_by_id id title
