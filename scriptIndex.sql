ALTER TABLE Book
ADD BookTitle AS JSON_VALUE(BookInfo, '$.BookTitle');

ALTER TABLE Book
ADD Author AS JSON_VALUE(BookInfo, '$.Author');

ALTER TABLE Book
ADD BookDescription AS JSON_VALUE(BookInfo, '$.BookDescription');

ALTER TABLE Book
ADD PublishDate AS JSON_VALUE(BookInfo, '$.PublishDate');


CREATE INDEX IX_Book_BookTitle_Computed ON Book (BookTitle);

CREATE INDEX IX_Book_Author_Computed ON Book (Author);

CREATE INDEX IX_Book_BookDescription_Computed ON Book (BookDescription);

CREATE INDEX IX_Book_PublishDate_Computed ON Book (PublishDate);

