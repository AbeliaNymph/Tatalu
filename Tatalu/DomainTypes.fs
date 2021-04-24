module DomainTypes

type Card = 
    {
        id: int;
        title: string;
    }

type List = 
    {
        id: int;
        title: string;
        cards: Map<string, Card> option
    }

type Kanban = 
    {
        id: int;
        title: string;
        lists: Map<string, List> option
    }