UPDATE c
SET c.country_id = s.Country_id
FROM City c
JOIN State s ON c.State_id = s.Id;
