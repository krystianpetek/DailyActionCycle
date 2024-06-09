namespace DailyActionCycle.WebUI.Services;

public class ActionTemplateService
{
    private readonly List<ActionTemplate> actionTemplates = [];

    public Task<ActionTemplate?> GetActionTemplate(Guid id)
    {
        return Task.FromResult(actionTemplates.Find(x => x.Id == id));
    }

    public Task<List<ActionTemplate>> GetActionTemplates()
    {
        return Task.FromResult(actionTemplates);
    }

    public Task<ActionTemplate> AddActionTemplate(string name) 
    {
        var actionTemplate = new ActionTemplate(name)
        {
            Position = actionTemplates.Count + 1
        };

        actionTemplates.Add(actionTemplate);
        
        return Task.FromResult(actionTemplate);
    }

    public Task<ActionTemplate> UpdateActionTemplate(ActionTemplate actionTemplate)
    {
        //var template = actionTemplates.Find(a => a.Id == actionTemplate.Id);

        //if (template is not null) { 
        //    //template.Position = actionTemplate.Position;
        //    //template.Name = actionTemplate.Name;
            
        //    //template.Activities.Clear();
        //    //template.Activities.AddRange(actionTemplate.Activities);

        //    return Task.FromResult(template);
        //}

        return Task.FromResult(actionTemplate);
    }
}

public sealed record class ActionTemplate(string Name)
{
    public int Position { get; set; }

    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; set; } = Name;

    public List<Activity> Activities { get; init; } = [];
}
