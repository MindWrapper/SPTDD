﻿IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRunStats]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetRunStats]
