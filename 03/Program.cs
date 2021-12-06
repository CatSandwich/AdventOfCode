using Common;

// Count the 1s and 0s in each digit place in a given set of binary values
IEnumerable<Count> GetCounts(string[] nums)
{
    for(var i = 0; i < nums[0].Length; i++)
    {
        yield return new Count() { Zero = nums.Count(num => num[i] == '0'), One = nums.Count(num => num[i] == '1') };
    }
}

var input = IO.Read("exit").ToArray();

// ----- PART 1 ----- //
var counts = GetCounts(input).ToArray();

// Get most common chars as array, then convert to int
var gammaArray = counts.Select(count => count.Max()).ToArray();
var gamma = Convert.ToInt32(new string(gammaArray), 2);

// Get least common chars as array, then convert to int
var epsilonArray = counts.Select(dict => dict.Min()).ToArray();
var epsilon = Convert.ToInt32(new string(epsilonArray), 2);

Console.WriteLine($"Part 1: {gamma * epsilon}");

// ----- PART 2 ----- //
var filteredOxygen = input;
var filteredCO2 = input;

for (var i = 0; i < input[0].Length; i++)
{
    // Recount 1s and 0s in digits
    var oxygenCount = GetCounts(filteredOxygen).ToArray();
    var co2Count = GetCounts(filteredCO2).ToArray();

    var high = oxygenCount[i].Max();
    var low = co2Count[i].Min();
    
    // Filter
    if(filteredOxygen.Length > 1) filteredOxygen = filteredOxygen.Where(num => num[i] == high).ToArray();
    if(filteredCO2.Length > 1) filteredCO2 = filteredCO2.Where(num => num[i] == low).DefaultIfEmpty(filteredCO2.Last()).ToArray();
}

// Convert binary char arrays to int
var oxygen = Convert.ToInt32(new string(filteredOxygen[0]), 2);
var co2 = Convert.ToInt32(new string(filteredCO2[0]), 2);

Console.WriteLine($"Part 2: {oxygen * co2}");

class Count
{
    public int Zero = 0;
    public int One = 0;

    public char Max() => Zero > One ? '0' : '1';
    public char Min() => One < Zero ? '1' : '0';
}