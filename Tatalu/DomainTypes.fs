module DomainTypes

type Card = 
    {
        id: int;
        title: string;
        list_id: int;
    }

type List = 
    {
        id: int;
        title: string;
        kanban_id: int;
    }

type Kanban = 
    {
        id: int;
        title: string;
        
    }