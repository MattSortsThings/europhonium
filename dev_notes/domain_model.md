# Domain model

This document outlines the domain model for the *Europhonium* project.

- [Domain model](#domain-model)
  - [Entities and Aggregates](#entities-and-aggregates)
  - [Country aggregate](#country-aggregate)
    - [Country aggregate types](#country-aggregate-types)
    - [Country aggregate key invariants](#country-aggregate-key-invariants)
    - [Country aggregate database schema](#country-aggregate-database-schema)
  - [Contest aggregate](#contest-aggregate)
    - [Contest aggregate types](#contest-aggregate-types)
    - [Contest aggregate key invariants](#contest-aggregate-key-invariants)
    - [Contest aggregate database schema](#contest-aggregate-database-schema)
  - [Broadcast aggregate](#broadcast-aggregate)
    - [Broadcast aggregate types](#broadcast-aggregate-types)
    - [Broadcast aggregate key invariants](#broadcast-aggregate-key-invariants)
    - [Broadcast aggregate database schema](#broadcast-aggregate-database-schema)

## Entities and Aggregates

The domain comprises 6 entity types, of which 3 are aggregates.

- A **COUNTRY** aggregate represents a specific country that exists in the world.
  - A **COUNTRY** aggregate is responsible for tracking its membership of **CONTEST** and **BROADCAST** aggregates.
- A **CONTEST** aggregate represents a specific year's edition of the Eurovision Song Contest.
  - A **CONTEST** aggregate is responsible for creating its constituent **BROADCAST** aggregates and tracking their completion status.
- A **BROADCAST** aggregate represents a competitive broadcast transmitted as a specific stage of a specific contest.
  - A **BROADCAST** aggregate is responsible for accumulating the points awarded in the broadcast and reporting its completion status.
- A **Participant** entity represents a specific country that participates in a specific contest.
- A **Competitor** entity represents a specific country that competes in a specific broadcast.
- A **Voter** entity represents a specific country that votes in a specific broadcast.

The key relationships between the entity types are shown in the diagram below:

```mermaid
erDiagram

COUNTRY
CONTEST
BROADCAST

Participant
Competitor
Voter

BROADCAST ||--|{ Competitor : owns
BROADCAST ||--|{ Voter : owns
COUNTRY ||--o{ Participant : is
COUNTRY ||--o{ Competitor : is
COUNTRY ||--o{ Voter : is
CONTEST ||--|{ BROADCAST : creates
CONTEST ||--|{ Participant : owns
```

- A single **CONTEST** aggregate creates one or more **BROADCAST** aggregates in the system.
- A single **CONTEST** aggregate owns multiple **Participant** entities.
- A single **BROADCAST** aggregate owns multiple **Competitor** entities.
- A single **BROADCAST** aggregate owns multiple **Voter** entities.
- A single **COUNTRY** aggregate is zero or more **Participant** entities in the system.
- A single **COUNTRY** aggregate is zero or more **Competitor** entities in the system.
- A single **COUNTRY** aggregate is zero or more **Voter** entities in the system.

## Country aggregate

### Country aggregate types

```mermaid
classDiagram

direction LR

class Country {
  <<AggregateRoot>>
  + Id : CountryId
  + CountryCode : CountryCode
  + Name : string
  + ContestMemberships : IReadOnlyCollection~ContestMembership~
  + BroadcastMemberships : IReadOnlyCollection~BroadcastMembership~
  + Create() ICountryBuilder$
  + UpsertMembership(ContestMembership) void
  + UpsertMembership(BroadcastMembership) void
  + RemoveMembership(ContestId) void
  + RemoveMembership(BroadcastId) void
}

class CountryId {
  <<ValueObject>>
  + Value : Guid
}

class CountryCode {
  <<ValueObject>>
  + Value : string
}

class ContestMembership {
  <<ValueObject>>
  + ContestId : ContestId
  + VotingOnly : bool
}

class BroadcastMembership {
  <<ValueObject>>
  + BroadcastId : BroadcastId
  + VotingOnly : bool
}

CountryId "1" --o "1" Country
CountryCode "1" --* "1" Country
ContestMembership "0..n" --o "1" Country
BroadcastMembership "0..n" --o "1" Country
```

### Country aggregate key invariants

- A `Country`'s `Id` is its system-wide unique identifier.
- Every `Country` in the system has a different `CountryCode`.
- A `Country` cannot be deleted from the system if its `ContestMemberships` collection is not empty *or* its `BroadcastMemberships` collection is not empty.
- If an item is added to a `Country`'s `ContestMemberships` collection with the same `ContestId` as an existing item, it replaces the existing item in the collection.
- If an item is added to a `Country`'s `BroadcastMemberships` collection with the same `BroadcastId` as an existing item, it replaces the existing item in the collection.
- A `CountryCode`'s `Value` is a string of 2 upper-case letters.

### Country aggregate database schema

```mermaid
erDiagram

country {
  id uniqueidentifier PK "non-clustered"
  country_code char(2) UK "clustered"
  name varchar(200) "not null"
}

contest_membership {
  id integer PK "clustered, generated on add"
  country_id uniqueidentifier FK "not null"
  contest_id uniqueidentifier "not null"
  voting_only bit "not null"
}

broadcast_membership {
  id integer PK "clustered, generated on add"
  country_id uniqueidentifier FK "not null"
  broadcast_id uniqueidentifier "not null"
  voting_only bit "not null"
}

country ||--o{ contest_membership : owns
country ||--o{ broadcast_membership : owns
```

**Additional constraints:**

| Table                | Details                              |
|:---------------------|:-------------------------------------|
| broadcast_membership | (country_id, broadcast_id) is unique |
| contest_membership   | (country_id, contest_id) is unique   |

## Contest aggregate

### Contest aggregate types

```mermaid
classDiagram

direction LR

class Contest {
  <<AggregateRoot>>
  + Id : ContestId
  + ContestYear : ContestYear
  + HostCityName : string
  + VotingRules : VotingRules
  + Completed : bool
  + Participants : IReadOnlyCollection~Participant~
  + Broadcasts : IReadOnlyCollection~BroadcastMemo~
  + Create() IContestBuilder$
  + CreateBroadcast() IBroadcastBuilder
  + UpsertBroadcast(BroadcastMemo) void
  + RemoveBroadcast(BroadcastId) void
  + GetMemberData() IEnumerable~ContestMemberDatum~
}

class ContestId {
  <<ValueObject>>
  + Value : Guid
}

class ContestYear {
  <<ValueObject>>
  + Value : int
}

class Participant {
  <<Entity>>
  + Id : CountryId
  + VotingOnly : bool
  + PerformerName : string?
  + SongTitle : string?
  + VotingRoles : IReadOnlyCollection~VotingRole~
}

class BroadcastMemo {
  <<ValueObject>>
  + BroadcastId : BroadcastId
  + ContestStage : ContestStage
  + Completed : bool
}

class VotingRole {
  <<ValueObject>>
  + ContestStage : ContestStage
  + TelevoteOnly : bool
}

class VotingRules {
  <<Enum>>
  + Undefined : 0
  + Stockholm : 2016
  + Liverpool : 2023
}

class ContestMemberDatum {
  <<Struct>>
  + CountryId : CountryId
  + ContestMembership : ContestMembership
}

ContestId "1" --o "1" Contest
ContestYear "1" --* "1" Contest
Participant "6..n" --* "1" Contest
BroadcastMemo "0..3" --o "1" Contest
VotingRole "1..3" --* "1" Participant
ContestMemberDatum "n" <-- Contest : creates
```

### Contest aggregate key invariants

- A `Contest`'s `Id` is its system-wide unique identifier.
- Every `Contest` in the system has a different `ContestYear`.
- A `Contest` cannot be deleted from the system if its `Broadcasts` collection is not empty.
- A `Contest` has at least 6 `Participants`, each of which has a different `Id` referencing an existing `Country` aggregate.
- A `Contest`'s `Participants` must have at least 3 voting in each contest stage.
- If an item is added to a `Contest`'s `Broadcasts` collection with the same `BroadcastId` as an existing item, it replaces the existing item in the collection.
- Every time a `Contest`'s `Broadcasts` collection is updated, its `Completed` property is set to `true` if the `Broadcasts` collection contains three items all of which have `Completed` = `true`, otherwise it is set to `false`.
- A `ContestYear`'s `Value` is an integer in the range \[2016, 2015\].

### Contest aggregate database schema

```mermaid
erDiagram

contest {
  id uniqueidentifier PK "non-clustered"
  contest_year integer UK "clustered"
  host_city_name varchar(200) "not null"
  voting_rules varchar(30) "not null, enum"
  completed bit "not null"
}

participant {
  contest_id uniqueidentifier PK,FK "non-clustered"
  id uniqueidentifier PK "non-clustered"
  votingOnly bit "not null"
  performer_name varchar(200)
  song_title varchar(200)
}

broadcast_memo {
  id integer PK "clustered, generated on add"
  contest_id uniqueidentifier FK "not null"
  broadcast_id uniqueidentifier "not null"
  contest_stage varchar(30) "not null, enum"
  completed bit "not null"
}

voting_role {
  id integer PK "clustered, generated on add"
  participant_contest_id uniqueidentifier FK "not null"
  participant_id uniqueidentifier FK "not null"
  contest_stage varchar(30) "not null, enum"
  televote_only bit "not null"
}

contest ||--|{ participant : owns
contest ||--o{ broadcast_memo : owns
participant ||--|{ voting_role : owns
```

**Additional constraints:**

| Table          | Details                                                           |
|:---------------|:------------------------------------------------------------------|
| broadcast_memo | (contest_id, contest_stage) is unique                             |
| voting_role    | (participant_contest_id, participant_id, contest_stage) is unique |

## Broadcast aggregate

### Broadcast aggregate types

```mermaid
classDiagram

direction LR

class Broadcast {
  <<AggregateRoot>>
  + Id : BroadcastId
  + ContestId : ContestId
  + ContestStage : ContestStage
  + TransmissionDate : DateOnly
  + TelevoteOnly : bool
  + Completed : bool
  + Competitors : IReadOnlyCollection~Competitor~
  + Voters : IReadOnlyCollection~Voter~
  + AddTelevoteAwards(CountryId, IList~CountryId~) ErrorOr~Updated~
  + AddJuryAwards(CountryId, IList~CountryId~) ErrorOr~Updated~
  + GetMemberData() IEnumerable ~BroadcastMemberDatum~
}

class BroadcastId {
  <<ValueObject>>
  + Value : Guid
}

class Competitor {
  <<Entity>>
  + Id : CountryId
  + PerformingOrder : int
  + FinishingOrder : int
  + TelevotePoints : int
  + JuryPoints : int
  + TotalPoints : int
  + TelevoteAwards : IReadOnlyCollection~TelevoteAward~
  + JuryAwards : IReadOnlyCollection~JuryAward~
}

class Voter {
  <<Entity>>
  + Id : CountryId
  + CanAwardTelevotePoints : bool
  + CanAwardJuryPoints : bool
}

class TelevoteAward {
  <<ValueObject>>
  + VoterId : CountryId
  + PointsValue : PointsValue
}

class JuryAward {
  <<ValueObject>>
  + VoterId : CountryId
  + PointsValue : PointsValue
}

class BroadcastMemberDatum {
  <<Struct>>
  + CountryId : CountryId
  + BroadcastMembership : BroadcastMembership
}

  class ContestStage {
    <<Enum>>
    + Undefined : 0
    + SemiFinal1 : 1
    + SemiFinal2 : 2
    + GrandFinal : 0
  }

  class PointsValue {
    <<Enum>>
    + Zero : 0
    + One : 1
    + Two : 2
    + Three : 3
    + Four : 4
    + Five : 5
    + Six : 6
    + Seven : 7
    + Eight : 8
    + Ten : 10
    + Twelve : 12
  }

BroadcastId "1" --o "1" Broadcast
Competitor "2..n" --* "1" Broadcast
Voter "3..n" --* "1" Broadcast
TelevoteAward "0..n" --* "1" Competitor
JuryAward "0..n" --* "1" Competitor
BroadcastMemberDatum "n" <-- Broadcast : creates
```

### Broadcast aggregate key invariants

- A `Broadcast`'s `Id` is its system-wide unique identifier.
- Every `Broadcast` in the system has a different (`ContestId`, `ContestStage`) tuple.
- A `Broadcast` has at least 3 `Voters`, each of which has a different `Id`.
- A `Broadcast` has at least 2 `Competitors`, each of which has a different `Id` matching one of its `Voters`.
- Every time a set of televote/jury points is awarded in a `Broadcast`, the `FinishingOrder` values of all its `Competitors` are updated.
- A `Broadcast` sets its `Completed` value to `true` once all its `Voters` have `CanAwardTelevotePoints` = `false` and `CanAwardJuryPoints` = `false`.
- A `Voter` awards a set of points by ranking all the `Competitors` except the one with the same `Id` as itself (if present).

### Broadcast aggregate database schema

```mermaid
erDiagram

broadcast {
  id uniqueidentifier PK "non-clustered"
  contest_id integer UK "not null, clustered"
  contest_stage varchar(30) "not null, enum, clustered"
  transmission_date date "not null"
  televote_only bit "not null"
  completed bit "not null"
}

competitor {
  broadcast_id uniqueidentifier PK,FK "non-clustered"
  id uniqueidentifier PK "non-clustered"
  performing_order integer "not null, >= 1"
  finishing_order integer "not null, >= 1"
  televote_points integer "not null, >= 0"
  jury_points integer "not null, >= 0"
  total_points integer "not null, = televote_points + jury_points"
}

voter {
  broadcast_id uniqueidentifier PK,FK "non-clustered"
  id uniqueidentifier PK "non-clustered"
  can_award_televote_points bit "not null"
  can_award_jury_points bit "not null"
}

televote_award {
  id integer PK "clustered, generated on add"
  competitor_broadcast_id uniqueidentifier FK "not null"
  competitor_id uniqueidentifier FK "not null"
  voter_id uniqueidentifier "not null"
  points_value integer "not null, enum"
}

jury_award {
  id integer PK "clustered, generated on add"
  competitor_broadcast_id uniqueidentifier FK "not null"
  competitor_id uniqueidentifier FK "not null"
  voter_id uniqueidentifier "not null"
  points_value integer "not null, enum"
}

broadcast ||--|{ competitor : owns
broadcast ||--|{ voter : owns
competitor ||--o{ televote_award : owns
competitor ||--o{ jury_award : owns
```

**Additional constraints:**

| Table          | Details                                                      |
|:---------------|:-------------------------------------------------------------|
| competitor     | (broadcast_id, performing_order) is unique                   |
| televote_award | (competitor_broadcast_id, competitor_id, voter_id) is unique |
| televote_award | competitor_id &ne; voter_id                                  |
| jury_award     | (competitor_broadcast_id, competitor_id, voter_id) is unique |
| jury_award     | competitor_id &ne; voter_id                                  |
