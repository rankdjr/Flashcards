﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.DTO;

public class FlashCardDto
{
    public int CardID { get; set; }
    public string? Front { get; set; }
    public string? Back { get; set; }
}
