
var lessonJsonToHtml = (jsonString) => {
    let lessonHtml = "";

    var lessonContent = JSON.parse(jsonString);
    for (let i = 0; i < lessonContent.length; i++) {
        var currSection = lessonContent[i];
        var sectionString = "";
        var contentType = currSection["ContentType"];

        switch (contentType) {
            case "Text":
                sectionString += `<b>${currSection["Title"]}</b>`;
                sectionString += `<p>${currSection["Body"]}</p> `;
                sectionString = `<div>${sectionString}</div>`;
                lessonHtml += sectionString;
                break;
            case "Image":
                sectionString += `<img src="${currSection["Body"]}"/>`;
                sectionString = `<div>${sectionString}</div>`;
                lessonHtml += sectionString;
                break;
            case "Video":
                sectionString += `<iframe frameborder="0" src="${currSection["Body"]}" width="640" height="360" </iframe>`;
                sectionString = `<div>${sectionString}</div>`;
                lessonHtml += sectionString;
                break;
        }

    }
    return lessonHtml;
}
var sample = `[{"Title":"Introduction","ContentType":"Text","Body":"Section 1, Introduction Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit."},{"Title":"","ContentType":"Image","Body":"https://i.ibb.co/6DrksK2/soho-blog-1-488-3acaff.jpg"},{"Title":"Mechanism","ContentType":"Text","Body":"Section 2, Mechanism Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit."},{"Title":"","ContentType":"Video","Body":"chrome://www.youtube.com/embed/_Dz6iQwzwE4"}]`;
console.log(lessonJsonToHtml(sample))