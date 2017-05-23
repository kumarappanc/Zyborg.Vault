﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Zyborg.Vault.POSH
{
	[Cmdlet(VerbsCommon.Set, "Profile",
			DefaultParameterSetName = DefaultParamSet)]
	public class SetProfile : PSCmdlet
	{
		public const string DefaultParamSet = VaultBaseCmdlet.DefaultParamSet;
		public const string RemoveParamSet = "Remove";

		[Parameter(Mandatory = true, Position = 0)]
		public string SaveAs
		{ get; set; }

		[Parameter(Mandatory = false, Position = 1,
				ParameterSetName = DefaultParamSet)]
		public string VaultAddress
		{ get; set; }

		[Parameter(Mandatory = false, Position = 2,
				ParameterSetName = DefaultParamSet)]
		public string VaultToken
		{ get; set; }

		[Parameter(Mandatory = false,
				ParameterSetName = DefaultParamSet)]
		public string VaultProfile
		{ get; set; }

		[Parameter(Mandatory = false,
				ParameterSetName = DefaultParamSet)]
		public SwitchParameter Force
		{ get; set; }

		[Parameter(Mandatory = true,
				ParameterSetName = RemoveParamSet)]
		public SwitchParameter Remove
		{ get; set; }


		protected override void EndProcessing()
		{
			VaultProfile vp = null;
			if (!string.IsNullOrEmpty(VaultProfile))
			{
				vp = Global.GetVaultProfile(this, VaultProfile);
			}

			var addr = VaultAddress ?? vp?.VaultAddress;
			var token = VaultToken ?? vp?.VaultToken;

			Global.SetVaultProfile(this, SaveAs, Remove.IsPresent, Force.IsPresent, addr, token);
		}
	}
}
