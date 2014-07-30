using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using HWSessionState.Models;

namespace HWSessionState
{
    public  class ChatHub : Hub
    {
        public Task JoinGroup(string groupName)
        {
            return Groups.Add(Context.ConnectionId, groupName);
        }

        public Task LeaveGroup(string groupName)
        {
            return Groups.Remove(Context.ConnectionId, groupName);
        }
        public void Send(string id,string groupname,string newSource)
        {
            // Call the addNewMessageToPage method to update clients.
           // Clients.Others.returnTurn(turn);
            Clients.Group(groupname).changePicOnClickandMarkAsSuch(id,newSource);
            //Clients.All.
            Clients.OthersInGroup(groupname).enableButtonOnNewTurn();
            Clients.OthersInGroup(groupname).changeTurn();
        }
        public void SendDb(string groupname,string result)
        {
            Guid toComp=new Guid(groupname);
            using (ApplicationDbContext con = new ApplicationDbContext())
            {
                var Game = con.Games.Find(toComp);
                Game.Result = result;
                Game.Status=Status.Archived;
               
                con.SaveChanges();
            }
            
        }

        public void Win(string groupname, string message, string id,string newSource)
        {
            // Call the addNewMessageToPage method to update clients.
            // Clients.Others.returnTurn(turn);
            Clients.Group(groupname).changePicOnClickandMarkAsSuch(id, newSource);
            Clients.Group(groupname).weHaveWinner(message);
          //  Clients.Others.addNewFuck();
            //Clients.Others.changeTurn();
        }

    }
}