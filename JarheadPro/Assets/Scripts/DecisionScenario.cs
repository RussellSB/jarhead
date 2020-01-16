public class DecisionScenario
{
    private string decision;
    public bool isRepeatable;
    private string option1;
    private string option2;
    public string option1ID;
    public string option2ID;

    public bool isChildDecision = false;

    //public DecisionScenario(string decision, bool isRepeatable)
    //{
    //    this.decision = decision;
    //    option1 = "Yes";
    //    option2 = "No";
    //    this.isRepeatable = isRepeatable;
    //}
    //
    //public DecisionScenario(string decision, bool isRepeatable, bool isChildDesicion)
    //{
    //    this.decision = decision;
    //    option1 = "Yes";
    //    option2 = "No";
    //    this.isChildDecision = isChildDesicion;
    //    this.isRepeatable = isRepeatable;
    //}
    //
    //public DecisionScenario(string decision, string option1, string option2, bool isRepeatable)
    //{
    //    this.decision = decision;
    //    this.option1 = option1;
    //    this.option2 = option2;
    //    this.isRepeatable = isRepeatable;
    //}

    public DecisionScenario(string decision, bool isRepeatable, string option1ID, string option2ID, bool isChildDecision = false, string option1 = "Yes", string option2 = "No")
    {
        this.decision = decision;
        this.option1 = option1;
        this.option2 = option2;
        this.isRepeatable = isRepeatable;
        this.option1ID = option1ID;
        this.option2ID = option2ID;
        this.isChildDecision = isChildDecision;
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
