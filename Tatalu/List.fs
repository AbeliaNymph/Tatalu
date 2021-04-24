module List

let create id title seq = 
    {
        DomainTypes.List.id = id;
        DomainTypes.List.title = title;
        DomainTypes.List.cards = seq;
    }

let debug (list: DomainTypes.List) = 
    printfn "\t| %i | %s |" list.id list.title

    match list.cards with
    | None -> printfn ""
    | Some cd -> 
        cd
        |> Map.iter (fun k v -> Card.debug v)