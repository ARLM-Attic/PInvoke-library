using System;
using System.Xml.Serialization;

namespace Discuz.Web.Services.API
{
	[XmlRoot ("auth_getSession_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
	public class SessionInfo
	{
		[XmlElement ("session_key")]
		public string SessionKey;

		[XmlElement ("uid")]
		public long UId;
        
        [XmlElement("user_name")]
        public string UserName;

        [XmlElement("expires")]
		public long Expires;

        //[XmlIgnore ()]
        //public bool IsInfinite
        //{
        //    get { return Expires == 0; }
        //}	
		
		public SessionInfo ()
		{}

		// use this if you want to create a session based on infinite session
		// credentials
		public SessionInfo (string session_key, long uid)
		{
			this.SessionKey = session_key;
			this.UId = uid;
			this.Expires = 0;
		}
	}

    /*
    [XmlRoot("photos_getAlbums_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
	public class AlbumsResponse
	{
		[XmlElement ("album")]
		public Album[] album_array;

		[XmlIgnore ()]
		public Album[] Albums
		{
			get { return album_array ?? new Album[0]; }
		}

		[XmlAttribute ("list")]
		public bool List;
	}

	[XmlRoot ("photos_get_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
	public class PhotosResponse
	{
		[XmlElement ("photo")]
		public Photo[] photo_array;

		[XmlIgnore ()]
		public Photo[] Photos
		{
			get { return photo_array ?? new Photo[0]; }
		}

		[XmlAttribute ("list")]
		public bool List;
	}

	[XmlRoot ("photos_getTags_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
	public class PhotoTagsResponse
	{
		[XmlElement ("photo_tag")]
		public Tag[] tag_array;

		public Tag[] Tags
		{
			get { return tag_array ?? new Tag[0]; }
		}

		[XmlAttribute ("list")]
		public bool List;
	}
	[XmlRoot ("groups_get_response", Namespace = "http://nt.discuz.net/api/")]
	public class GroupsResponse
	{
		[XmlElement ("group")]
		public Group[] group_array;

		public Group[] Groups
		{
			get { return group_array ?? new Group[0]; }
		}

		[XmlAttribute ("list")]
		public bool List;
	}

	[XmlRoot ("groups_getMembers_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
	public class GroupMembersResponse
	{
		[XmlElement ("members")]
		public PeopleList Members;

		[XmlElement ("admins")]
		public PeopleList Admins;

		[XmlElement ("officers")]
		public PeopleList Officers;

		[XmlElement ("not_replied")]
		public PeopleList NotReplied;
	}
    */


    [XmlRoot("users_getInfo_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class UserInfoResponse
    {
        [XmlElement("user")]
        public User[] user_array;

        public User[] Users
        {
#if NET1
			get { return user_array  == null ? new User[0] : user_array; }
#else
            get { return user_array ?? new User[0]; }
#endif
        }

        [XmlAttribute("list")]
        public bool List;
    }

    [XmlRoot("users_getLoggedInUser_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class LoggedInUserResponse
    {
        [XmlText]
        public int Uid;

        [XmlAttribute("list")]
        public bool List;
    }

    public class ArgResponse
    {
        [XmlElement("arg")]
        public Arg[] Args;
 
        [XmlAttribute("list")]
        public bool List;
    }

    /*
	[XmlRoot ("events_get_response", Namespace="http://nt.discuz.net/api/", IsNullable=false)]
	public class EventsResponse 
	{
		[XmlElement ("event")]
		public Event[] event_array;

		public Event[] Events
		{
			get { return event_array ?? new Event[0]; }
		}
	
		[XmlAttribute ("list")]
		public bool List;
	}

	[XmlRoot ("events_getMembers_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
	public class EventMembersResponse
	{
		[XmlElement ("attending")]
		public PeopleList Attending;

		[XmlElement ("unsure")]
		public PeopleList Unsure;

		[XmlElement ("declined")]
		public PeopleList Declined;

		[XmlElement ("not_replied")]
		public PeopleList NotReplied;
	}

	[XmlRoot ("friends_get_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
	public class FriendsResponse
	{
		[XmlElement ("uid")]
		public int[] uids;

		[XmlIgnore ()]
		public int[] UIds
		{
			get { return uids ?? new int[0]; }
		}
	}

	[XmlRoot ("friends_areFriends_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
	public class AreFriendsResponse
	{ 
		[XmlElement ("friend_info")]
		public FriendInfo[] friend_infos;
	}
     * */
}
