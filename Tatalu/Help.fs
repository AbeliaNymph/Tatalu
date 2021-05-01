module Help

let tatalu _ =
    """用法: Tatalu <命令> [<参数>]

    命令：
        kanban <选项> [<参数>]  操作看板
        list <选项> [<参数>]    操作列表
        card <选项> [<参数>]    操作卡片
        
    """
    |> printfn "%s"

let kanban _ = 
    """用法: Tatalu kanban [命令] [<参数>]

    命令:
        list    显示所有看板
        create <看板标题>   创建看板
        modify <看板编号> <看板标题>    修改指定编号看板的标题
        delete <看板编号>   删除指定编号的看板
    
    """
    |> printfn "%s"

let list _ = 
    """用法: Tatalu list [命令] [<参数>]

    命令:
        list    显示所有列表
        create <列表标题>   创建列表
        modify <列表编号> <列表标题>    修改指定编号列表的标题
        delete <列表编号>   删除指定编号的看板
    
    """
    |> printfn "%s"

let card _ = 
    """用法: Tatalu card [命令] [<参数>]

    命令:
        list    显示所有卡片
        create <卡片标题>   创建卡片
        move <卡片编号> <目的列表编号>    移动指定编号卡片至目的列表
        modify <卡片编号> <卡片标题>    修改指定编号列表的标题
        delete <卡片编号>   删除指定编号的看板
    
    """
    |> printfn "%s"
