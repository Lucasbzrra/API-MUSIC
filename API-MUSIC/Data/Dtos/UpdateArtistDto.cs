﻿using System.ComponentModel.DataAnnotations;

namespace API_MUSIC.Data.Dtos
{
    public class UpdateArtistDto
    {

        [Required(ErrorMessage = "tamanho de caracteres invalido")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = " Data de nascimento invalida")]
        [Range(1908, 2022)]
        public int Yearofbirth { get; set; }



        private string _styleMusic;

        [Required(ErrorMessage = "obrigatorio colocar o estilo musical [Samba,Funk,Sertanejo,Rap]")]
        public string StyleMusic
        {
            get { return _styleMusic; }
            set
            {
                string ValueLower = value.ToLower();
                if (generos.Contains(ValueLower))
                {
                    _styleMusic = ValueLower;
                }
            }
        }
        private List<string> generos = new List<string> { "samba", "funk", "sertanejo", "rap" };

    }
}
