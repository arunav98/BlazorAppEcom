using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorAppEcom.Server.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { 1, "2 States: The Story of My Marriage commonly known as 2 States is a 2009 novel written by Chetan Bhagat.It is the story about a couple coming from two states in India, who face hardships in convincing their parents to approve of their marriage. Bhagat wrote this novel after quitting his job as an investment banker.", "https://upload.wikimedia.org/wikipedia/en/3/3a/2_States_-_The_Story_Of_My_Marriage.jpg", 100m, "2 States: The Story of My Marriage" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { 2, "The 3 Mistakes of My Life is the third novel written by Chetan Bhagat. The book was published in May 2008 and had an initial print-run of 420,000. The novel follows the story of three friends and is based in the city of Ahmedabad in western India. This is the third best seller novel by Chetan Bhagat.", "https://upload.wikimedia.org/wikipedia/en/1/1d/3_Mistakes_of_My_Life.jpg", 107m, "The 3 Mistakes of My Life" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { 3, "Half Girlfriend is an Indian English coming of age, young adult romance novel by Indian author Chetan Bhagat.[1] The novel, set in rural Bihar, New Delhi, Patna, and New York, is the story of a Bihari boy in quest of winning over the girl he loves. This is Bhagat's sixth novel which was released on 1 October 2014[4] by Rupa Publications.", "https://upload.wikimedia.org/wikipedia/en/c/c6/Half_Girlfriend.jpg", 108m, "Half Girlfriend" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
