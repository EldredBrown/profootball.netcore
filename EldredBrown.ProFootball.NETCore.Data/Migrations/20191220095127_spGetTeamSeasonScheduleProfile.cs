using Microsoft.EntityFrameworkCore.Migrations;

namespace EldredBrown.ProFootball.NETCore.Data.Migrations
{
    public partial class spGetTeamSeasonScheduleProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var spGetTeamSeasonScheduleProfile = @"
CREATE PROCEDURE GetTeamSeasonScheduleProfile
	@teamName nvarchar(max), 
	@seasonId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		tsop.ID,
        tsop.Opponent,
        tsop.GamePointsFor,
        tsop.GamePointsAgainst,
		tsop.OpponentWins,
		tsop.OpponentLosses,
		tsop.OpponentTies,
		CASE
			WHEN tsop.OpponentGames = 0
				THEN NULL
			ELSE (2 * tsop.OpponentWins + tsop.OpponentTies) / (2 * tsop.OpponentGames)
		END AS OpponentWinningPercentage,        
		CASE
			WHEN tsop.OpponentGames = 0
				THEN NULL
			ELSE tsop.OpponentGames * s.NumOfWeeksCompleted / tsop.OpponentGames
		END AS OpponentWeightedGames,
		CASE
			WHEN tsop.OpponentGames = 0
				THEN NULL
			ELSE tsop.OpponentPointsFor * s.NumOfWeeksCompleted / tsop.OpponentGames
		END AS OpponentWeightedPointsFor,
		CASE
			WHEN tsop.OpponentGames = 0
				THEN NULL
			ELSE tsop.OpponentPointsAgainst * s.NumOfWeeksCompleted / tsop.OpponentGames
		END AS OpponentWeightedPointsAgainst
	FROM
		dbo.GetTeamSeasonOpponentProfiles(@teamName, @seasonId) AS tsop,
		dbo.Seasons AS s
	WHERE s.ID = @seasonId
	ORDER BY tsop.ID
END
GO";

            migrationBuilder.Sql(spGetTeamSeasonScheduleProfile);

			var fnGetTeamSeasonOpponentProfiles = @"
CREATE FUNCTION GetTeamSeasonOpponentProfiles
(	
	@teamName nvarchar(max), 
	@seasonId int
)
RETURNS @tbl TABLE
(
	ID int,
	Opponent nvarchar(max),
	GamePointsFor int,
	GamePointsAgainst int,
	OpponentWins int,
	OpponentLosses int,
	OpponentTies int,
	OpponentGames float,
	OpponentPointsFor float,
	OpponentPointsAgainst float
)
AS
BEGIN
	INSERT INTO @tbl

	SELECT
		gbts.ID,
        gbts.OpponentName AS Opponent,
        gbts.TeamScore AS GamePointsFor,
        gbts.OpponentScore AS GamePointsAgainst,
		CASE
			WHEN gbts.TeamScore < gbts.OpponentScore
				THEN ts.Wins - 1
			ELSE ts.Wins
		END AS OpponentWins,
		CASE
			WHEN gbts.TeamScore > gbts.OpponentScore
				THEN ts.Losses - 1
			ELSE ts.Losses
		END AS OpponentLosses,
		CASE
			WHEN gbts.TeamScore = gbts.OpponentScore
				THEN ts.Ties - 1
			ELSE ts.Ties
		END AS OpponentTies,
		CASE
			WHEN ts.Games = 0
				THEN ts.Games
			ELSE ts.Games - 1
		END AS OpponentGames,
        ts.PointsFor - gbts.OpponentScore AS OpponentPointsFor,
        ts.PointsAgainst - gbts.TeamScore AS OpponentPointsAgainst
	FROM dbo.GetGamesByTeamAndSeason(@teamName, @seasonId) AS gbts
		LEFT JOIN dbo.TeamSeasons AS ts ON gbts.OpponentName = ts.TeamName

	RETURN
END
GO";

			migrationBuilder.Sql(fnGetTeamSeasonOpponentProfiles);

			var fnGetGamesByTeamAndSeason = @"
CREATE FUNCTION GetGamesByTeamAndSeason
(	
	@teamName nvarchar(max),
	@seasonId int
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT
		ID,
		GuestName AS TeamName,
		GuestScore AS TeamScore,
		HostName AS OpponentName,
		HostScore AS OpponentScore
	FROM dbo.Games
	WHERE GuestName = @teamName AND SeasonId = @seasonId
	UNION
	SELECT
		ID,
		HostName AS TeamName,
		HostScore AS TeamScore,
		GuestName AS OpponentName,
		GuestScore AS OpponentScore
	FROM dbo.Games
	WHERE HostName = @teamName AND SeasonId = @seasonId
)
GO";

			migrationBuilder.Sql(fnGetGamesByTeamAndSeason);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
        {
			var fnGetGamesByTeamAndSeason = @"DROP FUNCTION dbo.GetGamesByTeamAndSeason";
			migrationBuilder.Sql(fnGetGamesByTeamAndSeason);

			var fnGetTeamSeasonOpponentProfiles = @"DROP FUNCTION dbo.GetTeamSeasonOpponentProfiles";
			migrationBuilder.Sql(fnGetTeamSeasonOpponentProfiles);

			var spGetTeamSeasonScheduleProfile = @"DROP PROCEDURE dbo.GetTeamSeasonScheduleProfile";
			migrationBuilder.Sql(spGetTeamSeasonScheduleProfile);
		}
	}
}
