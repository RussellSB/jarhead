public class DecisionScenario
{
    private string decision;
    public bool isRepeatable;
    private string option1;
    private string option2;
    public string option1ID;
    public string option2ID;

    public DecisionScenario(string decision, bool isRepeatable, string option1ID, string option2ID, string option1 = "Yes", string option2 = "No")
    {
        this.decision = decision;
        this.option1 = option1;
        this.option2 = option2;
        this.isRepeatable = isRepeatable;
        this.option1ID = option1ID;
        this.option2ID = option2ID;
    }

    public string getDecision()
    {
        return this.decision;
    }

    public string getOption1()
    {
        return this.option1;
    }

    public string getOption2()
    {
        return this.option2;
    }
}
