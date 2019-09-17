using System;
using System.Collections.Generic;
using System.Linq;

namespace Fitbit.Entities
{
    public class Marcheur 
    {
        #region champs
        private int _id;
        private string _nom;
        private DateTime _dateDeNaissance;
        private DateTime _dateDerniereMarche;
        private List<BadgeObtenu> _lesBadgesObtenus;
        private List<Badge> _lesBadges;
        private List<Parcours> _lesParcours;
        #endregion
        #region accesseurs
        public int Id { get => _id; }
        public string Nom { get => _nom; set => _nom = value; }
        public DateTime DateDeNaissance { get => _dateDeNaissance;  }
        public DateTime DateDerniereMarche { get => _dateDerniereMarche;  }

      

        public List<Parcours> LesParcours { get => _lesParcours; }
        public List<Badge> LesBadges { get => _lesBadges; }
        public List<BadgeObtenu> LesBadgesObtenus { get => _lesBadgesObtenus;  }


        #endregion

        #region constructeurs

        /// <summary>
        /// 
        /// </summary>
        public Marcheur(List<Badge> badges)
        {
            _lesParcours = new List<Parcours>();
            _lesBadgesObtenus = new List<BadgeObtenu>();
            _lesBadges = badges;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <example> Marcheur m = new Marcheur( 1, "  fds dsf")</example>
        public Marcheur(int id, string name, List<Badge> badges) :this(badges)
        {
            _id = id;
            _nom = name;            
            
        }
        /// <summary>
        /// sdfesdsfdsfsdfdsfdsfdsdsfsdfsd
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nom"></param>
        /// <param name="dateNaissance"></param>
        public Marcheur(int id, string nom, DateTime dateNaissance, List<Badge> badges)
        {
            _lesParcours = new List<Parcours>();
            _lesBadgesObtenus = new List<BadgeObtenu>();
            _lesBadges = badges;
        }

        #endregion

        #region methodes
        public int AddParcours(DateTime date, int nombreDePas)
        {
            int badgeAjoute = 0;
            _lesParcours.Add(new Parcours(_lesParcours.Count, nombreDePas, this) { Date = date }) ;
            List<Badge> badges = VerifierBadges(_lesParcours[_lesParcours.Count - 1]);
            if (badges.Count!=0)
            {
                badgeAjoute = AjouterNewBadges(badges, DateTime.Now);
            }
            return badgeAjoute;

        }



        private List<Badge> VerifierBadges(Parcours parcours)
        {
            List<Badge> lstBadge = new List<Badge>();
            int distance = 0;
            foreach (Badge badge in _lesBadges)
            {
                if (badge.DistanceJournaliere <= parcours.NombreDePas)
                {
                    lstBadge.Add(badge);
                }
            }
            foreach (Parcours parcour in _lesParcours)
            {
                distance = distance + parcour.NombreDePas;
            }

            //distance = _lesParcours.Sum(p => p.NombreDePas);

            foreach (Badge badge in _lesBadges)
            {
                if (distance >= badge.DistanceCumulee)
                {
                    lstBadge.Add(badge);
                }
            }
            return lstBadge;

        }
        private int AjouterNewBadges(List<Badge> badges, DateTime date)
        {
      
            int badgeAjout = 0;
            foreach (Badge badge in badges)
            {
                 BadgeObtenu badgeObtenue = new BadgeObtenu();
                bool obtenu = false;

                foreach (BadgeObtenu badgeObtenu in _lesBadgesObtenus)
                {
                
                    if (badgeObtenu.Badge == badge)
                    {
                        obtenu = true;
                        badgeObtenu.Dates.Add(date);
                    }
                }
                if (!obtenu)
                {
                    badgeObtenue.Badge = badge;
                    badgeObtenue.Dates.Add(date);
                    LesBadgesObtenus.Add(badgeObtenue);
                    badgeAjout++;
                }
            }
            return badgeAjout; 
        }
            #endregion


        }
}
