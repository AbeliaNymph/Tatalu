module Card

let create id title list_id = 
    {
        DomainTypes.Card.id = id;
        DomainTypes.Card.title = title;
        DomainTypes.Card.list_id = list_id;
    }

