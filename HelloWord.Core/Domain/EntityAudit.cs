namespace HelloWord.Core.Domain;

public class EntityAudit: EntityBase
{
    public DateTimeOffset CreateAt { get; set; }

    public DateTimeOffset UpdateAt { get; set; }
}