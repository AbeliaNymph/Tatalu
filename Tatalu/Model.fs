module Model

    module Card = 
        type Card = 
            {
                id: int;
                title: string;
            }

        let create id title = 
            {
                id = id;
                title = title;
            }

        let display card = 
            printfn "| %i | %s |" card.id card.title

    module List = 

        type List = 
            {
                id: int;
                title: string;
                cards: Map<string, Card.Card> option
            }

        let create id title seq = 
            {
                id = id;
                title = title;
                cards = seq;
            }

        let display list = 
            printfn "| %i | %s |" list.id list.title

    module Kanban = 
        type Kanban = 
            {
                id: int;
                title: string;
                lists: Map<string, List.List> option
            }
        
        let create id title seq = 
            {
                id = id;
                title = title;
                lists = seq;
            }

        let empty _ =
            {
                id = 0;
                title = "";
                lists = None
            }

    module Manager = 

        type Manager = 
            {
                kanbans: Map<string, Kanban.Kanban>
            }

        let create seq = 
            {
                kanbans = Map seq;
            }

