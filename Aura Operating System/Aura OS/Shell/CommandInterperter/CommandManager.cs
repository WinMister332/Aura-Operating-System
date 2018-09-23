/*
* PROJECT:          Aura Operating System Development
* CONTENT:          Command Manager
* PROGRAMMER(S):    John Welsh <djlw78@gmail.com>, WinMister332
*/

using System;
using System.Collections.Generic;
using System.Text;
using CMD = WMCommandFramework.COSMOS;
using Aura_OS.Shell.CommandInterperter.AuraConsole;

namespace Aura_OS.Shell.CommandInterperter
{
	public class CommandManager
	{
		private CMD.CommandProcessor processor = null;
		
		public CommandManager()
		{
			//Initialize processor.
			processor = new CMD.CommandProcessor();
			processor.OSName = "Aura OS";
			//Check if any users exist, if not create and LOGIN to the SYSTEM account.
			var users = processor.GetTerminalUsers();
			//Create SYSTEM account.
			TerminalUser systemUser = new TerminalUser("system", "2843049246789243487873215394358703021649726723", true, Permissions.SystemPermissions());
			systemUser.Login(); //Available only in next update
			users.AddUsers(systemUser);
			processor.Message = new CMOS.InputMessage(
                		new CMOS.MessageText(ConsoleColor.Cyan, $"${processor.CurrentUser.Username}"), //Gets current logged in user.
               			new CMOS.MessageText(ConsoleColor.Blue, $"@{processor.OSName}"),
                		CMOS.MessageText.WhiteSpace(),
                		new CMOS.MessageText(ConsoleColor.Green, System.IO.Directory.GetCurrentDirectory())
        		);
			processor.DebugMode = true;
            		processor.Version = new ApplicationVersion("AuraOS", new CommandCopyright("Aura-Development-Team"), new CommandVersion(1,0,0, "BETA"));
			//Initialize invoker.
			var invoker = processor.GetInvoker();
			//Register commands.
			//-Get existing 'echo' and 'clear' commands to overwrite.
			var echoCmd = invoker.GetCommandByName("echo");
			var clrCmd = invoker.GetCommandByName("clear");
			invoker.RemoveCommands(new Command[] { clr, echo });
			invoker.AddCommand(new Command[] {new Clear(), new Echo()});
		}
		
		public void Process()
		{
			//Start the processor.
			if ((processor.CurrentUser == null))
				processor.Login();
			else
			{
				Console.Clear();
				processor.Process();
			}
		}
	}
}
