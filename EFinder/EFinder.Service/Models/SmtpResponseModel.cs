﻿namespace EFinder.Service.Models;

public class SmtpResponseModel
{
    public SmtpResponseModel(int status, string message)
    {
        Status = status;
        Message = message;
    }

    public SmtpResponseModel(string fullMessage)
    {
        Status = int.Parse(fullMessage[..3]);
        Message = fullMessage;
    }

    public int Status { get; set; }
    public string Message { get; set; }
}