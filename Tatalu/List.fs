module List

let create id title kanban_id = 
    {
        DomainTypes.List.id = id;
        DomainTypes.List.title = title;
        DomainTypes.List.kanban_id = kanban_id;
    }

