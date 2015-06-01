if exists(select * from sysobjects where id= object_id( '[usp_pagedResultSet]'))
drop PROCEDURE [dbo].[usp_pagedResultSet]
go
CREATE PROCEDURE [dbo].[usp_pagedResultSet]
  @datasrc nvarchar(200)
 ,@orderBy nvarchar(200)
 ,@fieldlist nvarchar(200) = '*'
 ,@filter nvarchar(200) = ''
 ,@pageNum int = 1
 ,@pageSize int = 10 
AS
  SET NOCOUNT ON
  DECLARE
     @STMT nvarchar(max) ,        -- SQL to execute
	 @recct INT

  IF LTRIM(RTRIM(@filter)) = '' SET @filter = '1 = 1'
  IF @pageSize IS NULL BEGIN
    SET @STMT =  'SELECT   ' + @fieldlist + 
                 'FROM     ' + @datasrc +
                 'WHERE    ' + @filter + 
                 'ORDER BY ' + @orderBy
    EXEC (@STMT)                 -- return requested records 
  END ELSE BEGIN
    SET @STMT =  'SELECT   @recct = COUNT(*)
                  FROM     ' + @datasrc + '
                  WHERE    ' + @filter
    EXEC sp_executeSQL @STMT, @params = N'@recct INT OUTPUT', @recct = @recct OUTPUT
    SELECT @recct AS recct       -- return the total # of records

    DECLARE
      @lbound int,
      @ubound int

    SET @pageNum = ABS(@pageNum)
    SET @pageSize = ABS(@pageSize)
    IF @pageNum < 1 SET @pageNum = 1
    IF @pageSize < 1 SET @pageSize = 1
    SET @lbound = ((@pageNum - 1) * @pageSize)
    SET @ubound = @lbound + @pageSize + 1
    IF @lbound >= @recct BEGIN
      SET @ubound = @recct + 1
      SET @lbound = @ubound - (@pageSize + 1) -- return the last page of records if                                               -- no records would be on the
                                              -- specified page
    END
    SET @STMT =  'SELECT  ' + @fieldlist + '
                  FROM    (
                            SELECT  ROW_NUMBER() OVER(ORDER BY ' + @orderBy + ') AS row, *
                            FROM    ' + @datasrc + '
                            WHERE   ' + @filter + '
                          ) AS tbl
                  WHERE
                          row > ' + CONVERT(varchar(9), @lbound) + ' AND
                          row < ' + CONVERT(varchar(9), @ubound)
    EXEC (@STMT)                 -- return requested records 
  END
/**
 sample usage: 	
 exec [usp_pagedResultSet] 'paycheck', 'paycheckid','*','',2,10
 exec [usp_pagedResultSet] 'paycheck', 'paycheckid','*','where paycheckid between ? and ?',2,10

 **/
go
