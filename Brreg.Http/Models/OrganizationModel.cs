using System;

namespace Brreg.Http.Models
{
    public class OrganizationModel
    {
        public string Organisasjonsnummer { get; set; }
        public string Navn { get; set; }
        public DateTime RegistreringsdatoEnhetsregisteret { get; set; }
        public DateTime Stiftelsesdato { get; set; }
        public bool RegistrertIMvaregisteret { get; set; }
    }
}