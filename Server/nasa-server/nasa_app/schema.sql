DROP TABLE IF EXISTS suitstatus;
DROP TABLE IF EXISTS warnings;

CREATE TABLE suitstatus (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  create_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  p_h2o_g INTEGER NOT NULL,
  heart_bpm INTEGER NOT NULL,
  p_sub DECIMAL NOT NULL,
  t_water TIME NOT NULL,
  p_sop INTEGER NOT NULL,
  p_h2o_l INTEGER NOT NULL,
  rate_o2 DECIMAL NOT NULL,
  p_o2 INTEGER NOT NULL,
  cap_battery INTEGER NOT NULL,
  rate_sop DECIMAL NOT NULL,
  t_sub INTEGER NOT NULL,
  t_oxygen TIME NOT NULL,
  v_fan INTEGER NOT NULL,
  p_suit DECIMAL NOT NULL,
  t_battery TIME NOT NULL
);

CREATE TABLE warnings (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  create_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  vent_error BOOLEAN NOT NULL DEFAULT FALSE,
  sspe BOOLEAN NOT NULL DEFAULT FALSE,
  o2_off BOOLEAN NOT NULL DEFAULT FALSE,
  fan_error BOOLEAN NOT NULL DEFAULT FALSE,
  sop_on BOOLEAN NOT NULL DEFAULT FALSE,
  vehicle_power BOOLEAN NOT NULL DEFAULT FALSE,
  h2o_off BOOLEAN NOT NULL DEFAULT FALSE
);