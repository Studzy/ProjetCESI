﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCESI.Core
{
    public class Ressource : EntiteBase
    {
        [DataMember]
        public DateTimeOffset DateCreation { get; set; }
        [DataMember]
        public DateTimeOffset DateModification { get; set; }
        [DataMember]
        public DateTimeOffset DateSuppression { get; set; }
        [DataMember]
        public string Titre { get; set; }
        [DataMember]
        public string Contenu { get; set; }
        [DataMember]
        public Statut Statut { get; set; }
        [DataMember]
        public bool RessourceSupprime { get; set; }
        [DataMember]
        public int NombreConsultation { get; set; }
        [DataMember]
        public TypePartage TypePartage { get; set; }

        [DataMember]
        public User UtilisateurCreateur { get; set; }
        [DataMember]
        public int? UtilisateurCreateurId { get; set; }
        [DataMember]
        public TypeRessource TypeRessource { get; set; }
        [DataMember]
        public int TypeRessourceId { get; set; }
        [DataMember]
        public Categorie Categorie { get; set; }
        [DataMember]
        public int CategorieId { get; set; }

        public List<UtilisateurRessource> UtilisateurRessources { get; set; }
        public List<TypeRelationRessource> TypeRelationsRessources { get; set; }
        public List<Commentaire> Commentaires { get; set; }
        public List<HistoriqueRessource> HistoriqueRessources { get; set; }
    }

    public enum Statut
    {
        [Display(Name = "En pause")]
        EnPause,
        [Display(Name = "En attente de validation")]
        AttenteValidation,
        [Display(Name = "Accepté")]
        Accepter,
        [Display(Name = "Refusé")]
        Refuser
    }

    public enum TypePartage
    {
        Public,
        Partage,
        Prive
    }

    public enum TypeTriBase
    {
        [Display(Name = "Du plus ancien au plus récent")]
        DateModification,
        [Display(Name = "Du plus récent au plus ancien")]
        DateModificationDesc,
        [Display(Name = "De A à Z")]
        Titre,
        [Display(Name = "De Z à A")]
        TitreDesc,
        [Display(Name = "Les moins consultés")]
        NombreConsultation,
        [Display(Name = "Les plus consultés")]
        NombreConsultationDesc
    }
}