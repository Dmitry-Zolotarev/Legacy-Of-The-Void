public static class CombatCalculator
{
    public static int CalculatePlayerPower(CharacterData player)
    {
        return player.stats.body * 2 + player.stats.qi + player.stats.spirit;
    }
}