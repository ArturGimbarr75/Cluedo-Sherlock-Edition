using System;

[Flags]
public enum Suspect
{
	None                       = 0b_0000_0000,
	DetectiveInspectorLestrade = 0b_0000_0001,
	MrsHudson                  = 0b_0000_0010,
	IreneAdler                 = 0b_0000_0100,
	JohnWatson                 = 0b_0000_1000,
	SherlockHolmes             = 0b_0001_0000,
	MycroftHolmes              = 0b_0010_0000
}
