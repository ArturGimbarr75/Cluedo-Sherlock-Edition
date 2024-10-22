public class Request
{
	public Suspect Suspect { get; set; }
	public Weapon Weapon { get; set; }
	public Location Location { get; set; }

	public PlayerInformation Requester { get; set; }
	public PlayerInformation Responder { get; set; }
	public PlayerInformation[] PlayersWithoutCards { get; set; }
}
