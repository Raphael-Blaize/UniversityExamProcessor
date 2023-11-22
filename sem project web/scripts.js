var studentData = [];

function addStudent() {
    var student = {
        name: document.getElementById("studentName").value,
        idNumber: document.getElementById("idNumber").value,
        subjects: document.getElementById("subjects").value.split(',').map(subject => subject.trim()),
        scores: document.getElementById("scores").value.split(',').map(score => parseInt(score.trim()))
    };

    studentData.push(student);
    displayReport();
    clearForm();
    $('#addStudentModal').modal('hide');
}

function displayReport() {
    var reportContainer = document.getElementById("reportContainer");
    reportContainer.innerHTML = "<h2>Student Reports</h2>";

    for (var i = 0; i < studentData.length; i++) {
        var student = studentData[i];
        var averageScore = calculateAverageScore(student.scores);
        var grade = getGrade(averageScore);
        var recommendation = getRecommendation(averageScore);

        reportContainer.innerHTML += `
                    <table class="table">
                        <thead class="thead-light">
                            <tr>
                                <th scope="col">ID Number</th>
                                <th scope="col">Name</th>
                                <th scope="col">Report Date</th>
                                <th scope="col">Subjects and Scores</th>
                                <th scope="col">Average Score</th>
                                <th scope="col">Grade</th>
                                <th scope="col">Recommendation</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>${student.idNumber}</td>
                                <td>${student.name}</td>
                                <td>${getCurrentDate()}</td>
                                <td>${generateSubjectRows(student.subjects, student.scores)}</td>
                                <td>${averageScore.toFixed(2)}</td>
                                <td>${grade}</td>
                                <td>${recommendation}</td>
                            </tr>
                        </tbody>
                    </table>
                    <hr>
                `;
    }
}

function calculateAverageScore(scores) {
    var totalScores = scores.reduce(function (total, score) {
        return total + score;
    }, 0);

    return totalScores / scores.length;
}

function getGrade(averageScore) {
    if (averageScore >= 90) {
        return "A";
    } else if (averageScore >= 80) {
        return "B";
    } else if (averageScore >= 70) {
        return "C";
    } else if (averageScore >= 60) {
        return "D";
    } else {
        return "F";
    }
}

function getRecommendation(averageScore) {
    if (averageScore >= 80) {
        return "Excellent";
    } else if (averageScore >= 70) {
        return "Good";
    } else if (averageScore >= 60) {
        return "Average";
    } else {
        return "Poor";
    }
}

function generateSubjectRows(subjects, scores) {
    var rows = "";

    for (var i = 0; i < Math.max(subjects.length, scores.length); i++) {
        var subject = i < subjects.length ? subjects[i] : "";
        var score = i < scores.length ? scores[i] : "";

        rows += `<strong>${subject}</strong>: ${score}<br>`;
    }

    return rows;
}

function getCurrentDate() {
    var now = new Date();
    var month = now.getMonth() + 1;
    var day = now.getDate();
    var year = now.getFullYear();

    return month + "/" + day + "/" + year;
}

function clearForm() {
    document.getElementById("studentName").value = "";
    document.getElementById("idNumber").value = "";
    document.getElementById("subjects").value = "";
    document.getElementById("scores").value = "";
}