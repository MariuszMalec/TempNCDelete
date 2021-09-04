/*
 * Created by SharpDevelop.
 * User: 212517683
 * Date: 2/17/2020
 * Time: 10:30 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using System.IO;

namespace WyczyscTempNc
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{	
		public static string ncdir = (@"C:\tempNC\");
		public static List<string> listawszystkichplikow = new List<string>(new string[] {});			
		public static List<string> listakatalogow = new List<string>(new string[] {});			
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			listView1.Items.Clear();
			ListView1_Init_1();
			stworzlistekatalogow();
			stworzlisteplikow();
			pokazlistekatalogow();
			pokazlisteplikow();
			usunpliki();
			Application.Exit();
			Environment.Exit(1);			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
        private void ListView1_Init_1()
        {
            listView1.Clear();
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            //Add column header
            listView1.Columns.Add("Wyszukane pliki ", 600);

        }

		void usunpliki()
		{	
			if (listawszystkichplikow.Count > 0)
			{	
				if (MessageBox.Show ("CZY NA PEWNO Z KASOWAC ZAWARTOSC TEMPNC?", "ZAPYTANIE",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					foreach (string file in listawszystkichplikow)
					{	
				        string name = Path.GetFileName( file );
				        string dest = Path.Combine( ncdir, name );
				        if (System.IO.File.Exists(file))
				        {	
				           	File.Delete(dest);
				        }
					}
					usunkatalogi();
				}
			}
		}
		
		void usunkatalogi()
		{	
			if (listakatalogow.Count > 0)
			{	
				string[] katalogi = Directory.GetDirectories(ncdir);
	   	   		foreach (string katalog in katalogi)
	    	    {	
	   	   			if (System.IO.Directory.Exists(katalog))
	   	   			{
	   	   				Directory.Delete(katalog,true);
	   	   			}
	        	}
			}
		}
        
		void pokazlisteplikow()
		{	
			if (listawszystkichplikow.Count > 0)
			{
				foreach (string file in listawszystkichplikow)
				{	
		        	listView1.Items.Add(file);
	
				}
			}
		}
		
		void pokazlistekatalogow()
		{	
			if (listakatalogow.Count > 0)
			{
				foreach (string katalog in listakatalogow)
				{	
		        	listView1.Items.Add(katalog);
				}
			}
		}
        
		void stworzlisteplikow()
		{	
			listawszystkichplikow.Clear();
			string[] files = Directory.GetFiles(ncdir,"*.*", SearchOption.TopDirectoryOnly);
			foreach (string file in files)
			{	
			    string name = Path.GetFileName(file);
	        	listawszystkichplikow.Add(file);
			}
			
			foreach (string katalog in listakatalogow)
			{
				string dest = Path.Combine( ncdir, Path.GetFileName( katalog ));
				string[] files2 = Directory.GetFiles(dest);				
	        	foreach (string file in files2)
	        	{	
	        		listawszystkichplikow.Add(file);
	        	}
			}	
			
		}
		
		void stworzlistekatalogow()
		{	
			listakatalogow.Clear();
			string[] katalogi = Directory.GetDirectories(ncdir);
	        foreach (string katalog in katalogi)
	        {	
				listakatalogow.Add(katalog);
	        }
		}
		
	}
}
