var sampleHtml = `<p><b>Introduction</b></p><p style="margin-right: 0px; margin-bottom: 15px; margin-left: 0px; padding: 0px; text-align: justify; color: rgb(0, 0, 0); font-family: &quot;Open Sans&quot;, Arial, sans-serif;">Section 1, Introduction Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit.</p><p style="margin-right: 0px; margin-bottom: 15px; margin-left: 0px; padding: 0px; text-align: justify; color: rgb(0, 0, 0); font-family: &quot;Open Sans&quot;, Arial, sans-serif;"><br></p><p style="margin-right: 0px; margin-bottom: 15px; margin-left: 0px; padding: 0px; text-align: justify; color: rgb(0, 0, 0); font-family: &quot;Open Sans&quot;, Arial, sans-serif;"><img src="https://i.ibb.co/6DrksK2/soho-blog-1-488-3acaff.jpg" style="width: 488px;"><br></p><div><br></div><div><b>Mechanism</b></div><div><span style="color: rgb(0, 0, 0); font-family: &quot;Open Sans&quot;, Arial, sans-serif; text-align: justify;">Section 2, Mechanism Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit.<br></span><iframe frameborder="0" src="//www.youtube.com/embed/_Dz6iQwzwE4" width="640" height="360" class="note-video-clip"></iframe><b><br></b></div><div><br></div>`;


var LessonHtmlToJson = (htmlString) => {
    //this is added so the to suppor the regex
    htmlString += "<b>#customstop#</b>"
    var temporalDivElement = document.createElement("div");
    temporalDivElement.innerHTML = htmlString;

    //get all headers
    var headers = temporalDivElement.querySelectorAll("b");
    var images = temporalDivElement.querySelectorAll("img");
    var videos = temporalDivElement.querySelectorAll("iframe");


    //get content in between
    const regexp = /<\/b>(.+?)<b>/gi;
    const contentMatches = (htmlString.match(regexp));

    var lessonContent = [];
    var imageIndex = 0;
    var videoIndex = 0;

    for (let i = 0; i < headers.length; i++)
    {
        let currHeader = headers[i].textContent;
        if (currHeader == "" || currHeader == "#customstop#") continue;

        lessonContent.push(
            createSection(currHeader, "Text", contentMatches[i].replace(/<[^>]+>/g, ''))
        );
        
        //check if there is a video or image tag in text
        const imagesMatch = contentMatches[i].match(/<img.+?>/gi);
        let numberOfImages = 0;
        if (imagesMatch) { numberOfImages = imagesMatch.length; }
        if (numberOfImages > 0) {
            for (let i = 0; i < numberOfImages; i++)
            {
                lessonContent.push(
                    createSection(images[imageIndex].alt, "Image", images[imageIndex].src)
                 );
                imageIndex++;
            }
        }
        //check for videos in the section
        const videoMatch = contentMatches[i].match(/<iframe.+?<\/iframe>/gi);
        let numberOfVideos = 0;
        if (videoMatch) { numberOfVideos = videoMatch.length };
           if (numberOfVideos > 0) {
                for (let i = 0; i < numberOfVideos; i++)
                {
                    lessonContent.push(
                        createSection(videos[videoIndex].title, "Video", videos[videoIndex].src)
                    );
                videoIndex++;
            }
        }
    }
    console.log(JSON.stringify(lessonContent));
    return lessonContent;
}
const createSection = (title = "", ContentType = "", body = "" ) => {
    return {
        Title: title,
        ContentType: ContentType,
        Body: body
    };
}
console.log(LessonHtmlToJson(sampleHtml));
