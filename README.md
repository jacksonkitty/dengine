# Dengine

This is phase 2 of a half of a larger project, currently split between two worlds. This is a draft of the document for all of this half.

## Summary

Note that this will expand with time.

This is a multi-component project meant to help with personal (and household) data, tasks and other management. Beware of bot.

### SSDD

For initial iterations, this project uses a Solo Spiral Driven Development metholodogy.

### Phase expectations

Phase Speed: Quick
Quality Target: Low
Test Coverage: Not yet

Too early for: backups, logging, authnz, proper docker, errors, tests, love, lunch

### Quick schedule

 * [🌱] Quick Config
 * [🌱] Quick Client
 * [🌱] Quick Steno
 * [🌱] Quick Bot
 * [🌱] Blazor Web
 * [🌱] PS script
 * [ ] [REDACTED]
 * [ ] [REDACTED]

## History

Wave 1 - Tools (Quick Config + Client, Quick Steno)
Wave 2 - Basics (Satchel initial, Quick Bot)

## Components

This is summarized by the two solution files. Satchel (which contains Satchel) and everything else in Quick (a home for basic tools and stubs).

### Satchel

A simple (for now) backend for note display.

### Quick Bot

A default bot to be expanded on and forked from.

### Quick Config Service

This is a small tool to act as a central helper to act as an ad hoc source of truth for config when devs are starting up complex projects.

### Quick Config Client

This is a basic console app to test and/or demo usage of the service.

### Quick Steno

A very simple speech to text helper for use with a Stream Deck.
