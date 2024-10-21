using System;

[Flags]
public enum Weapon
{
	None        = 0b_0000_0000,
	Dagger      = 0b_0000_0001,
	Candlestick = 0b_0000_0010,
	Revolver    = 0b_0000_0100,
	Rope        = 0b_0000_1000,
	LeadPipe    = 0b_0001_0000,
	Wrench      = 0b_0010_0000
}
