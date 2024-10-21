using System;

[Flags]
public enum Location
{
	None                  = 0b_0000_0000,
	Baskerville           = 0b_0_0000_0001,
	BakerStreet221B       = 0b_0_0000_0010,
	TheTowerOfLondon      = 0b_0_0000_0100,
	IrenesFlat            = 0b_0_0000_1000,
	TheLab                = 0b_0_0001_0000,
	BatterseaPowerStation = 0b_0_0010_0000,
	Dartmoor              = 0b_0_0100_0000,
	MrsHudsonsKitchen     = 0b_0_1000_0000,
	TheSwimmingPool       = 0b_1_0000_0000
}