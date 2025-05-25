﻿namespace NoteTakingApp.Endpoints.Dtos;

public record RegisterUserDto
{
    public required string Email { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
}