module Card



let create id title = 
    {   
        DomainTypes.Card.id = id;
        DomainTypes.Card.title = title;
    }

let debug (card: DomainTypes.Card) = 
    printfn "\t\t- %i - %s -" card.id card.title